import { Component } from '@angular/core';
import { faReply } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-rules',
  templateUrl: './rules.component.html',
  styleUrls: ['./rules.component.less'],
})

export class RulesComponent {
  faReply = faReply;
  currentSlide = 1;
  
  plusSlides(n: number) {
    this.currentSlide += n;
    if (this.currentSlide > 3) {
      this.currentSlide = 1;
    } else if (this.currentSlide < 1) {
      this.currentSlide = 3;
    }
  }

  setSlide(n: number) {
    this.currentSlide = n;
  }
  
}

