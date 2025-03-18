using GongSolutions.Wpf.DragDrop;
using GongSolutions.Wpf.DragDrop.Utilities;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
//using Livet;

namespace HaisyaDesktop.ViewModel
{
    /// <summary>
    /// ViewModelの基底クラス
    /// INotifyPropertyChanged と IDataErrorInfo を実装する
    /// </summary>
    public abstract class BaseViewModel : BindableBase, IDisposable
    {
        //Livet.ViewModel,
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        //private readonly CompositeDisposable _cd = new();

        //public void Dispose() => this._cd.Dispose();


        //public override void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    base.RaisePropertyChanged(propertyName);
        //}

        //public abstract void DragOver(IDropInfo dropInfo);

        //public abstract void Drop(IDropInfo dropInfo);


        private bool _isBusy;
        /// <summary>
        /// 処理中の時true
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set {
                _isBusy = value;
                RaisePropertyChanged(nameof(IsBusy));
            }
        }

        protected Context.User GetUserData()
        {
            if (Context.ContextManager.Instance.User.UserId > 0)
            {
                return Context.ContextManager.Instance.User;
            } else
            {
                MessageBox.Show("ログイン認証エラー：システムを終了します。再度起動してください。");
                System.Windows.Application.Current.Shutdown();
                return null;
            }

        }


        protected void UpdateErrors([CallerMemberName] string propertyName = "", string errorMessage = "")
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                _errors.Remove(propertyName);
            }
            else
            {
                if (!_errors.ContainsKey(propertyName))
                {
                    _errors[propertyName] = new List<string>();
                }

                _errors[propertyName].Add(errorMessage);
            }

            RaiseErrorsChanged(propertyName);
        }

        public void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }



        #region == implemnt of ICommand Helper ==

        #region ** Class : _DelegateCommand
        // ICommand実装用のヘルパークラス
        private class _DelegateCommand : ICommand
        {
            private Action<object> _Command;        // コマンド本体
            private Func<object, bool> _CanExecute;  // 実行可否

            // コンストラクタ
            public _DelegateCommand(Action<object> command, Func<object, bool> canExecute = null)
            {
                if (command == null)
                    throw new ArgumentNullException();

                _Command = command;
                _CanExecute = canExecute;
            }

            // ICommand.Executeの実装
            void ICommand.Execute(object parameter)
            {
                _Command(parameter);
            }

            // ICommand.Executeの実装
            bool ICommand.CanExecute(object parameter)
            {
                if (_CanExecute != null)
                    return _CanExecute(parameter);
                else
                    return true;
            }

            // ICommand.CanExecuteChanged の実装
            event EventHandler ICommand.CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }
        #endregion

        // コマンドの生成
        protected ICommand CreateCommand(Action<object> command, Func<object, bool> canExecute = null)
        {
            return new _DelegateCommand(command, canExecute);
        }

        #endregion





        public bool HasErrors => throw new NotImplementedException();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// プロパティーの値をコピーする
        /// </summary>
        /// <param name="toObject"></param>
        /// <param name="fromObject"></param>
        /// <param name="notExistsPropertyNames"></param>
        public static void CopyProperty(object toObject, object fromObject, string notExistsPropertyNames = null)
        {
            // コピー元、コピー先のプロパティ情報を取得
            PropertyInfo[] fromProperties = fromObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] toProperties = toObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo fromProperty in fromProperties)
            {
                bool flgCopy = true;

                if (notExistsPropertyNames != null)
                {
                    foreach (string s in notExistsPropertyNames.Split(","))
                    {
                        if (s.Equals(fromProperty.Name)) { flgCopy = false; continue; }
                    }
                }

                if (flgCopy)
                {
                    if (fromProperty.PropertyType.FullName.StartsWith("System."))
                    {
                        // 名前と型が同じプロパティを取得
                        PropertyInfo target = Array.Find(toProperties, to => to.Name.Equals(fromProperty.Name)
                                                         && to.PropertyType.Equals(fromProperty.PropertyType));
                        // プロパティ値コピー
                        if (target != null)
                            target.SetValue(toObject, fromProperty.GetValue(fromObject));
                    }
                    else
                    {
                        object fromPropertySub = fromProperty.GetValue(fromObject);
                        object toPropertySub = null;
                        try
                        {
                            var checkFlg = toProperties.FirstOrDefault(x => x.Name == fromProperty.Name);
                            if (checkFlg != null)
                            {
                                toPropertySub = toProperties.FirstOrDefault(x => x.Name == fromProperty.Name).GetValue(toObject);
                            }
                        }
                        catch { }

                        if (fromPropertySub != null && toPropertySub != null)
                        {
                            CopyProperty(toPropertySub, fromPropertySub, notExistsPropertyNames);
                        }

                    }

                }

            }

        }







        public void Dispose()
        {
            Disposable.Dispose();
        }
    }
}
