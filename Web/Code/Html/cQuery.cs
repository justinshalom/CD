namespace Web.Code.Html
{
    using System;

    /// <summary>
    /// Jquery Like Dom Manipulations
    /// </summary>
    public class cQuery
    {
        /// <summary>
        /// The selector
        /// </summary>
        private string _selector;

        /// <summary>
        /// The HTML
        /// </summary>
        private string _htmlData;

        /// <summary>
        /// Initializes a new instance of the <see cref="cQuery" /> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="html">The HTML.</param>
        public cQuery(string selector, string html)
        {
            this.find(selector);
            this._htmlData = html;
            this._selector = selector;
        }

        /// <summary>
        /// Finds the specified selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        public cQuery find(string selector)
        {
            selector = selector;
            return this;
        }

        /// <summary>
        /// Adds the class.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        internal cQuery addClass(string className)
        {
            var content = this._htmlData;
            string[] separators = { "genid='"+this._selector+"'" };
            var array = content.Split(separators,StringSplitOptions.None);
            ////var temparray=array[1].Split('>');
            ////temparray[0].
            
            ////string[] stringarray = {"class="};
            ////line.Split(new string[1]{""})}
            return this;
        }

        /// <summary>
        /// Removes the class.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <returns></returns>
        internal cQuery removeClass(string className)
        {
            return this;
        }

        /// <summary>
        /// Attributes the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal cQuery attr(string name, string value)
        {
            return this;
        }

        /// <summary>
        /// Removes the attribute.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        internal cQuery removeAttr(string name)
        {
            return this;
        }

        /// <summary>
        /// CSSs the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal cQuery css(string name, string value)
        {
            return this;
        }

        /// <summary>
        /// Appends the specified HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        internal cQuery append(string html)
        {
            return this;
        }

        /// <summary>
        /// Prepends the specified HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        internal cQuery prepend(string html)
        {
            return this;
        }

        /// <summary>
        /// HTMLs the specified HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        internal cQuery html(string html = "")
        {
            return this;
        }

        /// <summary>
        /// Texts the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        internal cQuery text(string text = "")
        {
            return this;
        }


        /// <summary>
        /// Nexts the specified selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        internal cQuery next(string selector)
        {
            return this;
        }

        /// <summary>
        /// Previouses the specified selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        internal cQuery prev(string selector)
        {
            return this;
        }

        /// <summary>
        /// Closests the specified selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        internal cQuery closest(string selector)
        {
            return this;
        }

        /// <summary>
        /// Eqs the specified selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        internal cQuery eq(string selector)
        {
            return this;
        }
    }
}