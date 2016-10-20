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

namespace LibreCrewAssignment
{
    /// <summary>
    /// Indicates the type of assignment for a slot.
    /// </summary>
    enum AssignmentType
    {
        /// <summary>
        /// Assign by name.
        /// </summary>
        Name,

        /// <summary>
        /// Assign by profession (e.g. Scientist, Engineer).
        /// </summary>
        Profession,

        /// <summary>
        /// Assign by kerbal type (e.g. Crew, Tourist).
        /// </summary>
        KerbalType,

        /// <summary>
        /// Don't assign, leave slot empty.
        /// </summary>
        Empty
    }
}
