using System;
using System.Collections.Generic;
using System.Text;
using kin_kinit_mocker.Model.Spend;

namespace kin_kinit_mocker.Repository
{
    internal class OffersRepository
    {
        private List<Offer> _offers;
        public int OffersCount => _offers.Count;
        public bool IsEmpty => _offers.Count == 0;
        public OffersRepository()
        {
            _offers = new List<Offer>();
        }

        public Offer Offer(int index)
        {
            return _offers[index];
        }

        public void UpdateOffers(List<Offer> newOfferList)
        {
            if (IsDifferent(newOfferList))
            {
                _offers = newOfferList;
            }
           
        }

        private bool IsDifferent(List<Offer> diffValue)
        {
            if (diffValue.Count != _offers.Count) return true;

            for (int i = 0; i < _offers.Count; i++)
            {
                if (_offers[i] != diffValue[i])
                    return true;
            }

            return false;
        }
        public bool HasValidOffer(int index)
        {
            return index >= 0 && index < _offers.Count;
        }
    }
}
