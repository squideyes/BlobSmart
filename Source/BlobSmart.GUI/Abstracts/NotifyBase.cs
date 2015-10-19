using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace BlobSmart.GUI
{
    public abstract class NotifyBase<TM> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged<TR>(Expression<Func<TM, TR>> property)
        {
            if (PropertyChanged != null)
            {
                var propertyName = ((MemberExpression)property.Body).Member.Name;

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void RaiseNotifyEvent(EventHandler handler)
        {
            if (handler != null)
                handler(this, new NotifyArgs());
        }

        protected void RaiseNotifyEvent(EventHandler handler, NotifyArgs e)
        {
            if (handler != null)
                handler(this, e);
        }

        protected void RaiseNotifyEvent(EventHandler<NotifyArgs> handler)
        {
            if (handler != null)
                handler(this, new NotifyArgs());
        }

        protected void RaiseNotifyEvent(
            EventHandler<NotifyArgs> handler, NotifyArgs e)
        {
            if (handler != null)
                handler(this, e);
        }

        protected void RaiseNotifyEvent<T>(
            EventHandler<NotifyArgs<T>> handler, NotifyArgs<T> e)
        {
            if (handler != null)
                handler(this, e);
        }
    }
}
