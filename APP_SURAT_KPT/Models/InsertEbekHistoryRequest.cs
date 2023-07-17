using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APP_SURAT_KPT.Models
{
    public class InsertEbekHistoryRequest
    {
        public List<FamilyKPP> familyKPP { get; set; }
        public List<FamilyRekanan> familyRekanan { get; set; }
        public List<Coi> coi { get; set; }
    }

    public class Coi
    {
        public int Nomor { get; set; }
        public bool Jabatan { get; set; }
        public bool Department { get; set; }
        public bool Keluarga { get; set; }
    }

    public class FamilyKPP
    {
        public string Nama { get; set; }
        public string Jabatan { get; set; }
        public string Department { get; set; }
        public string Relasi { get; set; }
        public string Type { get; set; }
    }

    public class FamilyRekanan
    {
        public string Nama { get; set; }
        public string Jabatan { get; set; }
        public string Perusahaan { get; set; }
        public string Relasi { get; set; }
        public string Type { get; set; }
    }
}