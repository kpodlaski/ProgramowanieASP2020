﻿using System;
using System.Collections.Generic;
using System.Text;
using InformationBroker.Model;

namespace InformationBroker.Messages
{
    class OfferRequestMessage : Message
    {
        public readonly ProductInfo product;

        public OfferRequestMessage(String from, String to, ProductInfo product) : base(from, to, MessageType.OfferRequest)
        {
            this.product = product;
        }

    }
}
