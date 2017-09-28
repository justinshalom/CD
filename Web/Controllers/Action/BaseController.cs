namespace Web.Controllers.Action
{
    using System.Collections.Generic;
    using System.Data;
    using System.Web.Mvc;

    using Web.Code.Api.Objects;

    public class BaseController:Controller
    {
        /// <summary>
        /// Return the Output of JSON
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// JSON Data
        /// </returns>
        protected JsonResult OutPut(object data)
        {
            var rt = new ApiJsonDto
            {
                Data = data
            };
            return this.Json(rt);
        }
        protected JsonResult OutPut(object data,bool allowGet)
        {
            var rt = new ApiJsonDto
            {
                Data = data
            };
            if (allowGet)
            {
                return this.Json(rt,JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(rt);
            }
        }
        /// <summary>
        /// The out put.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        protected JsonResult OutPut(List<Dictionary<string, object>> data)
        {
            var rt = new DictionaryJsonDto
            {
                             Data = data
                         };
            return this.Json(rt);
        }

        /// <summary>
        /// The JSON.
        /// </summary>
        /// <param name="error">
        /// The error.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        protected JsonResult Json(bool error, string message, DataTable data)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in data.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in data.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }

                rows.Add(row);
            }

            return this.OutPut(rows);
        }

        /// <summary>
        /// The JSON.
        /// </summary>
        /// <param name="error">
        /// The error.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="dataSet">
        /// The data set.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        protected JsonResult Json(bool error, string message, DataSet dataSet)
        {
            if (dataSet.Tables.Count > 0)
            {
                var data = dataSet.Tables[0];
                return this.Json(error, message, data);
            }

            return this.OutPut(string.Empty);
        }
       
    }
}
