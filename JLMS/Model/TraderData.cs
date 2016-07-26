using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace JLMS.Model
{
    class TraderData : INotifyPropertyChanged
    {
        private string _trader;
        private double _buyalpha;
        private double _buyalphaincrement;
        private double _buybeta;
        private double _buybetaincrement;
        private double _buyreviewwaitfirst;
        private double _buyreviewwaitthen;
        private double _buyreviewwaitlast;
        private double _buymaxchange;
        private double _sellalpha;
        private double _sellalphaincrement;
        private double _sellbeta;
        private double _sellbetaincrement;
        private double _sellreviewwaitfirst;
        private double _sellreviewwaitthen;
        private double _sellreviewwaitlast;
        private double _sellmaxchange;
        private double _recent;
        private double _maxbidrule;
        private double _maxbidparameter;
        private double _minbidrule;
        private double _minbidparameter;

        public TraderData() { }
        public TraderData(string trader)
        {
            Trader = trader;
        }
        public string Trader
        {
            get { return _trader; }
            set { SetField(ref _trader, value); }
        }
        public double BuyAlpha
        {
            get { return _buyalpha; }
            set { SetField(ref _buyalpha, value); }
        }
        public double BuyAlphaIncrement
        {
            get { return _buyalphaincrement; }
            set { SetField(ref _buyalphaincrement, value); }
        }
        public double BuyBeta
        {
            get { return _buybeta; }
            set { SetField(ref _buybeta, value); }
        }
        public double BuyBetaIncrement
        {
            get { return _buybetaincrement; }
            set { SetField(ref _buybetaincrement, value); }
        }
        public double BuyReviewWaitFirst
        {
            get { return _buyreviewwaitfirst; }
            set { SetField(ref _buyreviewwaitfirst, value); }
        }
        public double BuyReviewWaitThen
        {
            get { return _buyreviewwaitthen; }
            set { SetField(ref _buyreviewwaitthen, value); }
        }
        public double BuyReviewWaitLast
        {
            get { return _buyreviewwaitlast; }
            set { SetField(ref _buyreviewwaitlast, value); }
        }
        public double BuyMaxChange
        {
            get { return _buymaxchange; }
            set { SetField(ref _buymaxchange, value); }
        }
        public double SellAlpha
        {
            get { return _sellalpha; }
            set { SetField(ref _sellalpha, value); }
        }
        public double SellAlphaIncrement
        {
            get { return _sellalphaincrement; }
            set { SetField(ref _sellalphaincrement, value); }
        }
        public double SellBeta
        {
            get { return _sellbeta; }
            set { SetField(ref _sellbeta, value); }
        }
        public double SellBetaIncrement
        {
            get { return _sellbetaincrement; }
            set { SetField(ref _sellbetaincrement, value); }
        }
        public double SellReviewWaitFirst
        {
            get { return _sellreviewwaitfirst; }
            set { SetField(ref _sellreviewwaitfirst, value); }
        }
        public double SellReviewWaitThen
        {
            get { return _sellreviewwaitthen; }
            set { SetField(ref _sellreviewwaitthen, value); }
        }
        public double SellReviewWaitLast
        {
            get { return _sellreviewwaitlast; }
            set { SetField(ref _sellreviewwaitlast, value); }
        }
        public double SellMaxChange
        {
            get { return _sellmaxchange; }
            set { SetField(ref _sellmaxchange, value); }
        }
        public double Recent
        {
            get { return _recent; }
            set { SetField(ref _recent, value); }
        }
        public double MaxBidRule
        {
            get { return _maxbidrule; }
            set { SetField(ref _maxbidrule, value); }
        }
        public double MaxBidParameter
        {
            get { return _maxbidparameter; }
            set { SetField(ref _maxbidparameter, value); }
        }
        public double MinBidRule
        {
            get { return _minbidrule; }
            set { SetField(ref _minbidrule, value); }
        }
        public double MinBidParameter
        {
            get { return _minbidparameter; }
            set { SetField(ref _minbidparameter, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
