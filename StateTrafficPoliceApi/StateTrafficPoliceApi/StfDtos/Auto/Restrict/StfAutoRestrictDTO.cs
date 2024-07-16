namespace StateTrafficPoliceApi.StfDtos.Auto.Restrict
{
    public class StfAutoRestrictDTO
    {
        private Dictionary<string, string> _organistions = new()
        {
            { "0", "не предусмотренный код" },
            { "1", "Судебные органы" },
            { "2", "Судебный пристав" },
            { "3", "Таможенные органы" },
            { "4", "Органы социальной защиты" },
            { "5", "Нотариус" },
            { "6", "ОВД или иные правоохр. органы" },
            { "7", "ОВД или иные правоохр. органы (прочие)" }
        };

        private Dictionary<string, string> _constraints = new()
        {
            { "0", "" },
            { "1", "Запрет на регистрационные действия" },
            { "2", "Запрет на снятие с учета" },
            { "3", "Запрет на регистрационные действия и прохождение ГТО" },
            { "4", "Утилизация (для транспорта не старше 5 лет)" },
            { "5", "Аннулирование" }
        };

        public string Regname { get; set; }

        public string OsnOgr { get; set; }

        public string Gid { get; set; }

        public string Tsyear { get; set; }

        public string TsVIN { get; set; }

        public int CodDL { get; set; }

        public string Dateogr { get; set; }

        public string Ogrkod { get; set; }

        public string Tsmodel { get; set; }

        public int CodeTo { get; set; }

        public string Dateadd { get; set; }

        public string Phone { get; set; }

        public string Regid { get; set; }

        public string Divtype { get; set; }

        public string Divid { get; set; }

        public string Restriction => _constraints[Ogrkod];

        public string RestrictOrg => _organistions[Divtype];
    }
}