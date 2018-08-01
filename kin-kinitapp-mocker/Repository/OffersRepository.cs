using System;
using System.Collections.Generic;
using System.Text;
using kin_kinit_mocker.Model.Spend;

namespace kin_kinit_mocker.Repository
{
    public class OffersRepository
    {
        private List<Offer> _offers;
        public int NumOfOffers => _offers.Count;
        public OffersRepository()
        {
            _offers = new List<Offer>();
        }

        public Offer Offer(int index)
        {
            return _offers[index];
        }

        public void ReplaceOfferList(List<Offer> newOfferList)
        {
            _offers = newOfferList;
        }

        public bool HasValidOffer(int index)
        {
            return index >= 0 && index < NumOfOffers;
        }
    }
}
