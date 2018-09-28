import { Action } from '@ngrx/store';

export class CommonAction<Ttype, Tpayload> implements Action {
  readonly type: any;

  constructor(public readonly actionType: Ttype, public readonly payload: Tpayload = null) {
    this.type = actionType;
  }
}
