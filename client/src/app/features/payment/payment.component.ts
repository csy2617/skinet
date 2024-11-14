import { Component, inject, OnInit } from '@angular/core';
import { Stripe, StripeCardElement, StripeElements } from '@stripe/stripe-js';
import { StripeService } from '../../core/services/stripe.service';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.scss'
})
export class PaymentComponent implements OnInit {
  private stripe: Stripe | null = null;
  private cardElement: StripeCardElement | null = null;
  private elements: StripeElements | null = null;

  stripeService = inject(StripeService);

  async ngOnInit() {
    this.stripe = this.stripeService.getStripeInstance();

    if (this.stripe) {
      this.elements = this.stripe.elements();
      this.cardElement = this.elements.create('card');
      this.cardElement.mount('#card-element');
    }
  }

  async handlePayment() {
    const amount = 1000; // Example amount in cents

    this.stripeService.createPaymentIntent(amount).subscribe(async (result) => {
      const clientSecret = result.clientSecret;

      if (this.stripe && this.cardElement) {
        const { error, paymentIntent } = await this.stripe.confirmCardPayment(clientSecret, {
          payment_method: { card: this.cardElement },
        });

        if (error) {
          console.error('Payment failed', error.message);
        } else if (paymentIntent) {
          console.log('Payment successful!', paymentIntent);
          alert('Payment successful!');
        }
      }
    });
  }
}
