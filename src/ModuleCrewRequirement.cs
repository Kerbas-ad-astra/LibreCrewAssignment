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
    /// Placing this module on a part causes it to request that the vehicle it's on be crewed
    /// rather than uncrewed.
    /// </summary>
    public class ModuleCrewRequirement : PartModule
    {
        /// <summary>
        /// Indicates the profession that the part is requesting there be at least 1 of
        /// on the ship. Allowable values include literal profession names (e.g. "Scientist"),
        /// kerbal types (e.g. "Tourist"), or null (meaning "any kerbal is fine").
        /// </summary>
        [KSPField]
        public String profession = null;

        /// <summary>
        /// Indicates the relative importance of this requirement. This is used for resolving
        /// conflicts if different parts are requesting different professions and there aren't
        /// enough crew slots to go round.
        /// </summary>
        [KSPField]
        public int importance = 0;

        /// <summary>
        /// The minimum profession level to apply to this requirement. By default it's
        /// zero, meaning "any kerbal of the required profession will do." If set to something
        /// higher than that, it means "a kerbal satisfies this requirement only if at or above
        /// the required level."
        /// </summary>
        [KSPField]
        public int minimumLevel = 0;

        /// <summary>
        /// Gets the crew requirements of the specified part, if any.
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static List<ModuleCrewRequirement> CrewRequirementsOf(Part part)
        {
            return part.Modules.GetModules<ModuleCrewRequirement>();
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append((profession == null) ? "Any" : profession)
                .Append(minimumLevel)
                .Append("-")
                .Append(importance)
                .ToString();
        }

        public string Description
        {
            get
            {
                string useProfession = (profession == null) ? "any" : profession;
                return (minimumLevel > 0) ? ("level " + minimumLevel + " " + useProfession) : useProfession;
            }
        }

    }
}
