// stripe.service.ts
import { Injectable } from '@angular/core';
import { loadStripe, Stripe } from '@stripe/stripe-js';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StripeService {
  private stripe: Stripe | null = null;

  constructor(private http: HttpClient) {
    // loadStripe('pk_test_51QKuW7DzqOdkN1JlmXy2oVeRh366ZzNgVWiYmEVN5VdYow4cLnAKkoMtFLse5NQRzuHf4X5tTdVIBf9Iftg9x5fJ00T43YfiHA').then((stripe) => {
    //   this.stripe = stripe;
    // });
  }
  private stripePublishableKey = 'pk_test_51QKuW7DzqOdkN1JlmXy2oVeRh366ZzNgVWiYmEVN5VdYow4cLnAKkoMtFLse5NQRzuHf4X5tTdVIBf9Iftg9x5fJ00T43YfiHA';

  async getStripeInstance(): Promise<Stripe | null> {
    // Check if Stripe has already been initialized
    if (!this.stripe) {
      this.stripe = await loadStripe(this.stripePublishableKey);
    }
    return this.stripe;
  }

  createPaymentIntent(amount: number): Observable<any> {
    return this.http.post<any>('http://localhost:5000/api/payments/create-payment-intent', { amount });
  }

}
