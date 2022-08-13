﻿using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public class Submarine : Vessel, ISubmarine
    {
        private const double InitialArmor = 200;
        private bool submergeMode = false;
        private const double weaponIncrement = 40;
        private const double speedIncrement = 4;
        public Submarine(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, InitialArmor)
        {
        }

        public bool SubmergeMode
        {
            get { return submergeMode; }
            set { submergeMode = value; }
        }

        public void ToggleSubmergeMode()
        {
            if (SubmergeMode == false)
            {
                this.SubmergeMode = true;
                this.MainWeaponCaliber += weaponIncrement;
                this.Speed -= speedIncrement;
            }
            else
            {
                this.SubmergeMode = false;
                this.MainWeaponCaliber -= weaponIncrement;
                this.Speed += speedIncrement;
            }
        }
        public override void RepairVessel()
        {
            if (this.ArmorThickness < InitialArmor)
            {
                this.ArmorThickness = InitialArmor;
            }
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (this.SubmergeMode == true)
            {
                sb.AppendLine($"*Submerge mode: ON");
            }
            else
            {
                sb.AppendLine($"*Submerge mode: OFF");
            }
            return base.ToString() + sb.ToString();
        }
    }
}
