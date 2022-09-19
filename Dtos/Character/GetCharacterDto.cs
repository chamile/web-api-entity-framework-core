using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_EF_WEB_API.Dtos.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int defence { get; set; } = 10;
        public int Integligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.knight;
    }
}
