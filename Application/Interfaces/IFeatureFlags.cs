using Application.Common.Enums;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Interfaces
{
    public interface IFeatureFlags
    {
        bool IsEnabled(FeatureFlags featureFlags);
    }
}
