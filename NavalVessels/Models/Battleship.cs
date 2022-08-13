using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    internal class Battleship : Vessel, IBattleship
    {
        private const double InitialArmor = 300;
        private bool sonarMode = false;
        private const double weaponIncrement = 40;
        private const double speedIncrement = 5;
        public Battleship(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, InitialArmor)
        {

        }

        public bool SonarMode { get => this.sonarMode; set => this.sonarMode = value; }

        public void ToggleSonarMode()
        {
            if (SonarMode == false)
            {
                SonarMode = true;
                this.MainWeaponCaliber += weaponIncrement;
                this.Speed -= speedIncrement;
            }
            else
            {
                SonarMode = false;
                this.MainWeaponCaliber -= weaponIncrement;
                this.Speed += speedIncrement;
            }
        }
        public override void RepairVessel()
        {
            if (this.ArmorThickness < InitialArmor)
            {
                this.ArmorThickness += InitialArmor;
            }
        }
        public override string ToString()
        {

            var sb = new StringBuilder();
            sb.AppendLine($"- {this.Name}");
            sb.AppendLine($"*Type: {this.GetType().Name}");
            sb.AppendLine($"*Armor thickness: {this.ArmorThickness}");
            sb.AppendLine($"*Main weapon caliber: {this.MainWeaponCaliber}");
            sb.AppendLine($"*Speed: {this.Speed} knots");
            if (Targets.Count == 0)
            {
                sb.AppendLine($"*Targets: None");
            }
            else
            {
                sb.AppendLine($"*Targets: {String.Join(",", this.Targets)}");
            }
            if (this.SonarMode == true)
            {
                sb.AppendLine($"*Sonar mode: ON");
            }
            else
            {
                sb.AppendLine($"*Sonar mode: OFF");
            }
            return sb.ToString();

        }
    }
}
