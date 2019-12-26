import { Component, OnInit } from "@angular/core";
import { ValueService } from "../../services/value.service";

@Component({
  selector: "app-value",
  templateUrl: "./value.component.html",
  styleUrls: ["./value.component.css"]
})
export class ValueComponent implements OnInit {
  values: any;
  constructor(private valueService: ValueService) {}

  ngOnInit() {
    this.values = this.valueService.getValues().subscribe(values => {
      this.values = values;
      console.log("1", this.values);
    });
  }
}
