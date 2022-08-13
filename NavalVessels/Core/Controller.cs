using NavalVessels.Core.Contracts;
using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Core
{
    public class Controller : IController
    {
        private IRepository<IVessel> vessels;
        private Dictionary<string, ICaptain> captains;

        public Controller()
        {
            captains = new Dictionary<string, ICaptain>();
            vessels = new VesselRepository();
        }
        private IReadOnlyCollection<ICaptain> Captains => captains.Values;
        private IReadOnlyCollection<IVessel> Vessels => vessels.Models;
        public string HireCaptain(string fullName)
        {
            if (captains.ContainsKey(fullName))
            {
                return $"{string.Format(OutputMessages.CaptainIsAlreadyHired, fullName)}";
            }
            ICaptain captain = new Captain(fullName);
            captains.Add(captain.FullName, captain);
            return $"{string.Format(OutputMessages.SuccessfullyAddedCaptain, fullName)}";
        }
        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            if (vesselType != "Battleship" || vesselType != "Submarine")
            {
                return $"{string.Format(OutputMessages.InvalidVesselType)}";
            }
            IVessel vessel = vessels.FindByName(name);
            if (vessel == null)
            {
                if (vesselType == "Battleship")
                {
                    vessel = new Battleship(name, mainWeaponCaliber, speed);
                    return $"{string.Format(OutputMessages.SuccessfullyCreateVessel, vessel.GetType().Name, name, mainWeaponCaliber, speed)}";
                }
                else if (vesselType == "Submarine")
                {
                    vessel = new Submarine(name, mainWeaponCaliber, speed);
                    return $"{string.Format(OutputMessages.SuccessfullyCreateVessel, vessel.GetType().Name, name, mainWeaponCaliber, speed)}";
                }
            }
            return $"{string.Format(OutputMessages.VesselIsAlreadyManufactured, vessel.GetType().Name, name)}";
        }
        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            if (!captains.ContainsKey(selectedCaptainName))
            {
                return $"{string.Format(OutputMessages.CaptainNotFound, selectedCaptainName)}";
            }
            if (vessels.FindByName(selectedVesselName) == null)
            {
                return $"{string.Format(OutputMessages.VesselNotFound, selectedVesselName)}";
            }
            if (vessels.FindByName(selectedVesselName).Captain != null)
            {
                return $"{string.Format(OutputMessages.VesselOccupied, selectedVesselName)}";
            }

            var captain = captains.GetValueOrDefault(selectedCaptainName);
            var vessel = vessels.FindByName(selectedVesselName);
            captain.AddVessel(vessel);
            vessel.Captain = captain;

            return $"{string.Format(OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName)}";

        }
        public string CaptainReport(string captainFullName)
        {
            var captain = captains.GetValueOrDefault(captainFullName);
            return captain.Report();
        }
        public string VesselReport(string vesselName)
        {
            var vessel = vessels.FindByName(vesselName);
            return vessel.ToString();
        }
        public string ToggleSpecialMode(string vesselName)
        {
            var vessel = vessels.FindByName(vesselName);
            if (vessel == null)
            {
                return $"{string.Format(OutputMessages.VesselNotFound,vesselName)}";
            }
            if (vessel.GetType().Name == "Battleship")
            {
                return $"{string.Format(OutputMessages.ToggleBattleshipSonarMode, vesselName)}";
            }
            else
            {
                return $"{string.Format(OutputMessages.ToggleSubmarineSubmergeMode, vesselName)}";
            }
        }
        public string ServiceVessel(string vesselName)
        {
            var vessel = vessels.FindByName(vesselName);
            if (vessel == null)
            {
                return $"{string.Format(OutputMessages.VesselNotFound, vesselName)}";
            }
            vessel.RepairVessel();
            return $"{string.Format(OutputMessages.SuccessfullyRepairVessel, vesselName)}";
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            var attackingVessel = vessels.FindByName(attackingVesselName);
            var defendingVessel = vessels.FindByName(defendingVesselName);
            if (attackingVessel == null)
            {
                return $"{string.Format(OutputMessages.VesselNotFound, attackingVesselName)}";
            }
            if (defendingVesselName == null)
            {
                return $"{string.Format(OutputMessages.VesselNotFound, defendingVessel)}";
            }
            if (attackingVessel.ArmorThickness == 0)
            {
                return $"{string.Format(OutputMessages.AttackVesselArmorThicknessZero, attackingVesselName)}";
            }
            if (defendingVessel.ArmorThickness == 0)
            {
                return $"{string.Format(OutputMessages.AttackVesselArmorThicknessZero, defendingVesselName)}";
            }
            attackingVessel.Attack(defendingVessel);
            attackingVessel.Captain.IncreaseCombatExperience();
            defendingVessel.Captain.IncreaseCombatExperience();
            return $"{string.Format(OutputMessages.SuccessfullyAttackVessel, defendingVesselName, attackingVesselName, defendingVessel.ArmorThickness)}";
        }

        

        
        

        
    }
}
