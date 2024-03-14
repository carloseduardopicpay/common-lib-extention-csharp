using System;

namespace Infra.CrossCutting.Utils
{
    public static class DateTimeHelper
    {
        public static DateTime? ToDate(this string date)
        {
            if (String.IsNullOrEmpty(date))
                return null;

            var nDate = date.Replace("'", "").Split('/');
            return new DateTime(int.Parse(nDate[2]), int.Parse(nDate[1]), int.Parse(nDate[0]));

        }

        public static string ToBrazilianDate(this DateTime date)
        {

            return date.IsValid() ? date.ToString("dd/MM/yyyy") : string.Empty;
        }

        public static string ToBrazilianDateTime(this DateTime date)
        {

            return date.IsValid() ? date.ToString("dd/MM/yyyy hh:mm:ss") : string.Empty;
        }

        public static string ToBrazilianDate(this DateTime? date)
        {
            if (!date.HasValue)
                return string.Empty;

            return date.Value.IsValid() ? date.Value.ToString("dd/MM/yyyy") : string.Empty;
        }

        /// <summary>
        /// Retorna se a data de referência é uma data válida (o ano tem que ser maior que 1753
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsValid(this DateTime date)
        {
            return date > DateTime.MinValue;
        }

        /// <summary>
        /// Retorna NULL se a data for considerada inválida
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime? ToNull(this DateTime date)
        {
            if (!date.IsValid())
                return null;

            return date;
        }

        /// <summary>
        /// Retorna a mesma data porém com as horas setadas para o final do dia (23:59:59)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FullDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }

        public static bool HasValidDateValue(this DateTime? date)
        {
            if (!date.HasValue) return false;
            if (date.Value == DateTime.MinValue) return false;

            return true;
        }

        /// <summary>
        /// Verifica se o valor é compativel com o tipo DateTime do SQL Server
        /// Utiliza a biblioteca nativa do SQL para validar o range de valores.
        /// </summary>
        /// <param name="someval">A date string that may parse</param>
        /// <returns>true if the parameter is valid for SQL Sever datetime</returns>
        static bool IsValidSqlDateTime(this DateTime? date)
        {
            bool valid = (date.HasValue && date.Value > DateTime.MinValue);

            if (valid)
            {
                System.Data.SqlTypes.SqlDateTime sdt;
                try
                {
                    // take advantage of the native conversion
                    sdt = new System.Data.SqlTypes.SqlDateTime(date.Value);
                    valid = true;
                }
                catch (System.Data.SqlTypes.SqlTypeException)
                {
                    // no need to do anything, this is the expected out of range error
                }
            }

            return valid;
        }
    }
}
