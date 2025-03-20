using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal class Config

    {
        public string connectionString = "Server=DESKTOP-O9EER6A\\SQLEXPRESS;Database=3DPrinting;Integrated Security=true;TrustServerCertificate=true;";
        public string selectParent = "select * from producatori";
        public string selectChild = "select * from materiale";
        public string nameParent = "Producatori";
        public string nameChild = "Materiale";
        public string parentID = "id";
        public string parentReference = "producator";
        public string childID = "id";

        public string deleteChild = "Delete from materiale where id=@id";

    }
}
