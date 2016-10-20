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

using System.Collections.Generic;

namespace LibreCrewAssignment
{
    /// <summary>
    /// Represents the set of all crewable components on a ship.
    /// </summary>
    class CrewableList
    {
        private List<Crewable> allCrewables;
        private List<Crewable> commandCrewables;

        public CrewableList(ShipConstruct construct)
        {
            allCrewables = Crewable.List(construct);
            commandCrewables = new List<Crewable>();
            foreach (Crewable crewable in allCrewables)
            {
                if (crewable.IsCommand) commandCrewables.Add(crewable);
            }
        }

        public int Count { get { return allCrewables.Count; } }

        /// <summary>
        /// Determines whether the specified crew member is assigned to anything in the list.
        /// </summary>
        /// <param name="crewMember"></param>
        /// <returns></returns>
        public bool IsAssigned(ProtoCrewMember crewMember)
        {
            return Crewable.IsAssigned(allCrewables, crewMember);
        }

        /// <summary>
        /// Clears all crew assignments from the list.
        /// </summary>
        public void ClearAssignments()
        {
            foreach (Crewable crewable in allCrewables)
            {
                crewable.Clear();
            }
        }

        /// <summary>
        /// Determines whether any assignments remain to attempt.
        /// </summary>
        public bool NeedsCrew
        {
            get
            {
                foreach (Crewable crewable in allCrewables)
                {
                    if (crewable.NeedsCrew) return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Get all the crewables in the list.
        /// </summary>
        public IEnumerable<Crewable> All {  get { return allCrewables; } }

        /// <summary>
        /// Get all the command-pod crewables in the list.
        /// </summary>
        public IEnumerable<Crewable> Command {  get { return commandCrewables; } }
    }
}
