using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NotizBuch.SQL.Main
{

    // Implementiert die SQL Basis Methoden
    interface ISQLFunctions<T>
    {

        List<T> GetAll();

        T Get(int id);

        void Insert(T t);

        void Edit(T t);

        void Delete(int id);

    }
}
