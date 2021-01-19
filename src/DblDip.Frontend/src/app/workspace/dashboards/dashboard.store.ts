import { THIS_EXPR } from "@angular/compiler/src/output/output_ast";
import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { Dashboard } from "./dashboard";
import { DashboardsModule } from "./dashboards.module";


@Injectable()
export class DashboardStore {


    public dashboard$: Subject<Dashboard> = new Subject();

    public dashboards$: Subject<Dashboard[]> = new Subject();

    constructor() {

    }
}