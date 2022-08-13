using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Repositories.Contracts
{
    public class VesselRepository : IRepository<IVessel>
    {
        private Dictionary<string, IVessel> vessels;
        public VesselRepository()
        {
            this.vessels = new Dictionary<string, IVessel>();
        }
        public IReadOnlyCollection<IVessel> Models => this.vessels.Values;

        public void Add(IVessel model)
        {
            vessels.Add(model.Name, model);
        }

        public IVessel FindByName(string name)
        {
            if (vessels.ContainsKey(name))
            {
                return this.vessels.GetValueOrDefault(name);
            }
            return null;
        }

        public bool Remove(IVessel model)
        {
            if (vessels.ContainsValue(model))
            {
                vessels.Remove(model.Name, out model);
                return true;
            }
            return false;
        }
    }
}
