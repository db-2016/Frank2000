using System.Collections.Generic;

namespace FrankCodingTest.Data
{
    public interface IDTO
    {
        bool DeleteRecord(string query);        
        bool InsertRecord(string query);
        List<T> Select(string query);
        bool UpdateRecord(string query);
        void Dispose();
    }
}