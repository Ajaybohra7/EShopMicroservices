using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public  record Payment
    {
        public int PaymentMethod { get;  } = default!;
        public string CardNumber { get; } = default!;
        public string? CardName { get; } = default!;
        public string Expiration { get;  }= default!;
        public string CVV { get;  } = default!;

        protected Payment() { }
        private Payment(int paymentMethod, string cardNumber, string? cardName, string expiration, string cvv)
        {
            PaymentMethod = paymentMethod;
            CardNumber = cardNumber;
            CardName = cardName;
            Expiration = expiration;
            CVV = cvv;
        }
        public static Payment Of(int paymentMethod, string cardNumber, string? cardName, string expiration, string cvv)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(cardNumber);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(expiration);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(cvv);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length,3);   
            return new Payment(paymentMethod, cardNumber, cardName, expiration, cvv);
        }
    }
}
