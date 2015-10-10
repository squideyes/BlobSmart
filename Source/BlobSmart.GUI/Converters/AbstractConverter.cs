using System;
using System.Windows.Markup;

namespace BlobSmart.GUI
{
    public abstract class AbstractConverter : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
