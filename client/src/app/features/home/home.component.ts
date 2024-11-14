import { Component } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { PaymentComponent } from "../payment/payment.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    MatButton,
    RouterLink,
    PaymentComponent
],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}