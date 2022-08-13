using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Repositories.Contracts
{
    public class VesselRepository : IRepository<IVessel>
    {
        private ICollection<IVessel> vessels;
        public VesselRepository()
        {
            this.vessels = new HashSet<IVessel>();
        }
        public IReadOnlyCollection<IVessel> Models
        {
            get => (IReadOnlyCollection<IVessel>)this.vessels;
            private set => this.vessels = (ICollection<IVessel>)value;
        }

        public void Add(IVessel model)
        {
            vessels.Add(model);
        }

        public IVessel FindByName(string name)
        {
            foreach (var vessel in vessels)
            {
                if (vessel.Name == name)
                {
                    return vessel;
                }
            }
            return null;
        }

        public bool Remove(IVessel model)
        {
            if (vessels.Contains(model))
            {
                vessels.Remove(model);
                return true;
            }
            return false;
        }
    }
}
