import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { en_US, NZ_I18N } from 'ng-zorro-antd/i18n';
import { NgZorroAntdModule } from './ng-zorro-antd.module';
registerLocaleData(en)
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,NgZorroAntdModule],
  providers: [{ provide: NZ_I18N, useValue: en_US }],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ClientApp';
}
