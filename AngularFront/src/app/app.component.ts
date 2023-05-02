import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Item } from './models/item';
import { User } from './models/user';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  public forecasts?: WeatherForecast[];

  title = 'AngularFront';
  user = new User();

  constructor(private authService: AuthService) {}

  register(user: User) {
    this.authService.register(user).subscribe();
  }

  login(user: User) {
    this.authService.login(user).subscribe((token: string) => {
      localStorage.setItem('authToken', token);
    });
  }

  getMe() {
    this.authService.getMe().subscribe((name: string) => {
      console.log(name);
    });
  }

  getItems() {
    this.authService.getItems().subscribe((items: Array<Item>) => {
      console.log(items);
    });
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
