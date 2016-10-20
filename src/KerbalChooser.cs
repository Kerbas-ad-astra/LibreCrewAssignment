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
    interface KerbalChooser
    {
        /// <summary>
        /// Given two kerbals, choose the preferable one.
        /// </summary>
        /// <param name="kerbal1"></param>
        /// <param name="kerbal2"></param>
        /// <returns></returns>
        ProtoCrewMember Choose(ProtoCrewMember kerbal1, ProtoCrewMember kerbal2);
    }

    /// <summary>
    /// Choose the kerbal with highest experience.
    /// </summary>
    class HighExperienceChooser : KerbalChooser
    {
        public static HighExperienceChooser Instance = new HighExperienceChooser();
        private HighExperienceChooser() { }
        public ProtoCrewMember Choose(ProtoCrewMember kerbal1, ProtoCrewMember kerbal2)
        {
            if (kerbal1 == null) return kerbal2;
            if (kerbal2 == null) return kerbal1;
            return (kerbal1.experience >= kerbal2.experience) ? kerbal1 : kerbal2;
        }
    }

    /// <summary>
    /// Choose the pilot with the highest experience that's better than SAS, or the
    /// lowest experience that's worse than SAS.
    /// </summary>
    class PilotChooser : KerbalChooser
    {
        private static readonly string PILOT_PROFESSION = "pilot";
        private readonly int sasLevel;
        public static PilotChooser ForSasLevel(int sasLevel) { return new PilotChooser(sasLevel); }
        private PilotChooser(int sasLevel) { this.sasLevel = sasLevel; }

        public ProtoCrewMember Choose(ProtoCrewMember kerbal1, ProtoCrewMember kerbal2)
        {
            if (kerbal1 == null) return kerbal2;
            if (kerbal2 == null) return kerbal1;
            if (PILOT_PROFESSION.Equals(kerbal1.trait.ToLower()) && PILOT_PROFESSION.Equals(kerbal2.trait.ToLower()))
            {
                if ((kerbal1.experienceLevel > sasLevel) || (kerbal2.experienceLevel > sasLevel))
                {
                    return (kerbal1.experience >= kerbal2.experience) ? kerbal1 : kerbal2;
                }
                else
                {
                    return (kerbal1.experience <= kerbal2.experience) ? kerbal1 : kerbal2;
                }
            }
            else
            {
                return HighExperienceChooser.Instance.Choose(kerbal1, kerbal2);
            }
        }
    }
}
