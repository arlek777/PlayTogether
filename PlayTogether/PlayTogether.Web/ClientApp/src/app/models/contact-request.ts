export class ContactRequest {
  id: string;
  toUserId: string;
  userId: string;
  status: ContactRequestStatus;
}

export enum ContactRequestStatus {
  Open = 0,
  Approved,
  Rejected
}
