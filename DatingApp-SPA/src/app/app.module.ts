import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from "./app.component";
import { ValueComponent } from "./test/components/value/value.component";
import { ValueService } from "./test/services/value.service";

@NgModule({
  declarations: [AppComponent, ValueComponent],
  imports: [BrowserModule, HttpClientModule],
  providers: [ValueService],
  bootstrap: [AppComponent]
})
export class AppModule {}
