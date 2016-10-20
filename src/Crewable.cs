//
//  This file is part of LibreCrewAssignment.
//
//  Copyright (c) 2016 Kerbas-ad-astra
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;

namespace LibreCrewAssignment
{
    /// <summary>
    /// Represents a crewable component of a ship.
    /// </summary>
    class Crewable
    {
        private readonly Part part;
        private readonly PartCrewManifest manifest;
        private List<CrewSlot> crewSlots;

        /// <summary>
        /// Gets the crew capacity.
        /// </summary>
        public readonly int Capacity;

        public CrewSlot this[int index]
        {
            get { return crewSlots[index]; }
        }

        public int Count
        {
            get { return crewSlots.Count; }
        }

        private Crewable(Part part, PartCrewManifest manifest, int capacity)
        {
            this.part = part;
            this.manifest = manifest;
            Capacity = capacity;
            crewSlots = CrewSlot.GetSlots(part, manifest);
        }

        /// <summary>
        /// Gets whether we're in a state where we can generate a list of crewables. It would
        /// be nice if this were true all the time, but KSP's editor is flaky and there are
        /// several points in the state machine where the ship manifest hasn't caught up with
        /// the extant parts.
        /// 
        /// In particular, when you attach a new crewable part to the ship, you get the following
        /// sequence of events:
        /// 
        /// 1. a "ship modified" event that includes the part, but the manifest doesn't have it yet
        /// 2. a "part attached" event, this includes the part in the manifest
        /// </summary>
        /// <param name="construct"></param>
        /// <returns></returns>
        public static bool CanList(ShipConstruct construct)
        {
            if (ShipConstruction.ShipManifest == null) return false;
            if (construct == null) return false;

            int crewablePartCount = 0;
            foreach (Part part in construct.parts)
            {
                if (part.CrewCapacity > 0) ++crewablePartCount;
            }
            return crewablePartCount == ShipConstruction.ShipManifest.GetCrewableParts().Count;
        }

        /// <summary>
        /// Given a ship construct from the editor, get a list of crewables. Throws if
        /// there's an error.
        /// </summary>
        /// <param name="construct"></param>
        /// <returns></returns>
        public static List<Crewable> List(ShipConstruct construct)
        {
            // Get all the parts that are crewable
            List<Part> parts = new List<Part>();
            foreach (Part part in construct.parts)
            {
                if (part.CrewCapacity > 0) parts.Add(part);
            }

            // Get all the crewable parts from the vessel manifest and hope like hell that they're in the same order
            List<PartCrewManifest> manifests = ShipConstruction.ShipManifest.GetCrewableParts();
            if (parts.Count != manifests.Count)
            {
                throw new Exception("Mismatched lists: " + parts.Count + " crewable parts (out of " + construct.parts.Count + " total), " + manifests.Count + " part manifests");
            }
            List<Crewable> crewables = new List<Crewable>(parts.Count);
            for (int index = 0; index < parts.Count; ++index)
            {
                Part part = parts[index];
                PartCrewManifest manifest = manifests[index];
                String partName = part.partInfo.name;
                String manifestName = manifest.PartInfo.name;
                if (!partName.Equals(manifestName))
                {
                    throw new Exception("Mismatch at index " + index + ": " + partName + " versus " + manifestName);
                }
                int partCapacity = part.CrewCapacity;
                int manifestCapacity = manifest.GetPartCrew().Length;
                if (partCapacity != manifestCapacity)
                {
                    throw new Exception("Mismatched capacity for " + partName + ": " + partCapacity + " for part, " + manifestCapacity + " for manifest");
                }
                crewables.Add(new Crewable(part, manifest, partCapacity));
            }

            return crewables;
        }

        /// <summary>
        /// Ensure that all current assignments for the part will be persisted by making
        /// sure that there's a by-name ModuleCrewAssignment for every slot.
        /// </summary>
        public void PersistAssignments()
        {
            ModuleCrewAssignment module = ModuleCrewAssignment.Find(part);
            if (module == null) return;
            List<Assignment> assignments = new List<Assignment>(part.CrewCapacity);
            foreach (CrewSlot slot in Slots)
            {
                assignments.Add(Assignment.ForCrew(slot.Occupant));
            }
            module.slotAssignments = Assignment.ToString(assignments);
            Logging.Log("Persisted slot assignments for " + ToString() + ": " + module.slotAssignments);
        }

        /// <summary>
        /// Clears all crew out of the crewable.
        /// </summary>
        public void Clear()
        {
            for (int index = 0; index < Capacity; ++index)
            {
                manifest.RemoveCrewFromSeat(index);
            }
            crewSlots = CrewSlot.GetSlots(part, manifest);
        }

        /// <summary>
        /// Gets whether the crewable contains the specified crew member.
        /// </summary>
        /// <param name="crewMember"></param>
        /// <returns></returns>
        public bool IsAssigned(ProtoCrewMember crewMember)
        {
            return manifest.Contains(crewMember);
        }


        /// <summary>
        /// Gets whether any of the crewables contain the specified crew member.
        /// </summary>
        /// <param name="crewables"></param>
        /// <param name="crewMember"></param>
        /// <returns></returns>
        public static bool IsAssigned(IEnumerable<Crewable> crewables, ProtoCrewMember crewMember)
        {
            foreach (Crewable crewable in crewables)
            {
                if (crewable.IsAssigned(crewMember))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Find the first unoccupied slot. Returns null if there isn't one.
        /// </summary>
        /// <param name="crewables"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static CrewSlot FindEmptySlot(IEnumerable<Crewable> crewables)
        {
            foreach (Crewable crewable in crewables)
            {
                foreach (CrewSlot slot in crewable.Slots)
                {
                    if (slot.NeedsOccupant)
                    {
                        return slot;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets whether this is a command module.
        /// </summary>
        public bool IsCommand { get { return part.Modules.GetModules<ModuleCommand>().Count > 0; } }

        /// <summary>
        /// Gets whether any assignments remain to assign.
        /// </summary>
        public bool NeedsCrew
        {
            get
            {
                foreach (CrewSlot slot in Slots)
                {
                    if (slot.NeedsOccupant) return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Gets the crew slots in the crewable.
        /// </summary>
        public IEnumerable<CrewSlot> Slots { get { return crewSlots; } }

        public override string ToString()
        {
            return Logging.ToString(part);
        }
    }
}
