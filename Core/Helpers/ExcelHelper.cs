using ExcelDataReader;
using System.Data;
using System.Reflection;
using System.Text;

namespace Core.Helpers
{
    public static class ExcelHelper
    {
        public static List<T> ReadExcelFile<T>(Stream excelStream, string fileName)
        {
            var data = new List<T>();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            IExcelDataReader excelReader;

            if (Path.GetExtension(fileName).ToUpper() == ".XLS")
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(excelStream);
            }
            else
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(excelStream);
            }

            var conf = new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true
                }
            };

            var result = excelReader.AsDataSet(conf);
            var excelData = result.Tables[0];

            foreach (DataRow row in excelData.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }

            excelReader.Close();

            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        object value = dr[column.ColumnName];

                        if (value == DBNull.Value)
                        {
                            pro.SetValue(obj, null, null);
                        }
                        else
                        {
                            try
                            {
                                pro.SetValue(obj, value, null);
                            }
                            catch (Exception ex)
                            {
                                var errMsg = ex.Message.ToString();
                            }
                        }
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
