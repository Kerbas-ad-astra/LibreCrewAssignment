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
using System.Text;

namespace LibreCrewAssignment
{
    /// <summary>
    /// Adding this module to a crewed command pod specifies how the crew slots in the
    /// pod should be assigned.
    /// 
    /// An assignment is a string, which can be any of the following:
    /// 
    /// - A specific kerbal's name (e.g. "Jebediah Kerman").
    /// - A profession (e.g. "Engineer", "Scientist").
    /// - A kerbal type (e.g. "Crew", "Tourist")
    /// - "Empty" (this means "leave the slot without any crew in it").
    /// 
    /// Multiple values can be concatenated with "|", in which case each successive
    /// one will be tried until success is found or the end of the chain is reached.
    /// If no match is found, the slot is left empty.
    ///
    /// Examples:
    /// 
    /// "Jebediah Kerman|Pilot|Crew" means "assign Jeb if he's available,
    /// otherwise assign a pilot if available, otherwise assign any crew."
    /// 
    /// "Scientist|Engineer" means "assign a scientist if available, otherwise
    /// assign an engineer if available, otherwise use the default".
    /// 
    /// "Scientist|Engineer|Empty" means "assign a scientist if available, otherwise
    /// assign an engineer if available, otherwise leave the slot empty".
    /// </summary>
    public class ModuleCrewAssignment : PartModule
    {
        /// <summary>
        /// The default assignment for all slots in the command pod.
        /// </summary>
        [KSPField]
        public string defaultAssignment;

        /// <summary>
        /// A comma-delimited list of assignments for specific slots in the command pod.
        /// </summary>
        [KSPField(isPersistant = true)]
        public string slotAssignments;

        /// <summary>
        /// Find a ModuleCrewAssignment on the part, or null if there isn't one.
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static ModuleCrewAssignment Find(Part part)
        {
            List<ModuleCrewAssignment> modules = part.Modules.GetModules<ModuleCrewAssignment>();
            return (modules.Count == 0) ? null : modules[0];
        }
    }
}
