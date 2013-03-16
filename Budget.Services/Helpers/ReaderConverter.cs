using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Budget.Services.Helpers
{
    public class ReaderConverter<T> where T : new()
    {
        public T ConvertFromReader(SqlDataReader sqlDataReader)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SqlParameter> GetParams(T source)
        {
            IEnumerable<string> propNames = GetPropertiesNames(source);
            throw new NotImplementedException();
        }

        private IEnumerable<string> GetPropertiesNames(T source)
        {
            var props = source.GetType().GetProperties().Where(p => p.CanWrite);
            
            return props.Select(p => p.Name);
            throw new NotImplementedException();
        }
    }
}
