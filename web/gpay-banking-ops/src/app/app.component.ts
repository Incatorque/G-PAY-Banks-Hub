import { Component, signal } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  readonly mobileNavOpen = signal(false);

  toggleNavigation(): void {
    this.mobileNavOpen.update(open => !open);
  }

  closeNavigation(): void {
    this.mobileNavOpen.set(false);
  }
}
