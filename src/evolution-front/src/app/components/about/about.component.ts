import { Component } from '@angular/core';
import { faReply } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.less'],
})
export class AboutComponent {
  faReply = faReply;
}
