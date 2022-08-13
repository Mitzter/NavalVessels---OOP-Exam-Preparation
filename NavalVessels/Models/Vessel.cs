using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public abstract class Vessel : IVessel
    {
        private string name;
        private ICaptain captain;
        private double armorThickness;
        private double mainWeaponCaliber;
        private double speed;
        private ICollection<string> targets;
        protected Vessel(string name, double mainWeaponCaliber, double speed, double armorThickness)
        {
            this.Name = name;
            this.MainWeaponCaliber = mainWeaponCaliber;
            this.Speed = speed;
            this.ArmorThickness = armorThickness;
            targets = new HashSet<string>();
        }
        public string Name
        {
            get { return this.name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidVesselName);
                }
                this.name = value;
            }
        }

        public ICaptain Captain
        {
            get { return this.captain; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidCaptainToVessel);
                }
                this.captain = value;
            }
        }
        public double ArmorThickness
        {
            get => this.armorThickness;
            set => this.armorThickness = value;
        }

        public double MainWeaponCaliber
        {
            get => this.mainWeaponCaliber;
            protected set => this.mainWeaponCaliber = value;
        }

        public double Speed
        {
            get => this.speed;
            protected set => this.speed = value;
        }

        public ICollection<string> Targets => this.targets;

        public void Attack(IVessel target)
        {
            if (target == null)
            {
                throw new NullReferenceException(ExceptionMessages.InvalidTarget);
            }
            double targetArmor = target.ArmorThickness;
            double attackCalibre = this.MainWeaponCaliber;
            target.ArmorThickness -= attackCalibre;

            if (target.ArmorThickness < 0)
            {
                target.ArmorThickness = 0;
            }
            Targets.Add(target.Name);
        }

        public abstract void RepairVessel();

        public abstract override string ToString();
    }
}
