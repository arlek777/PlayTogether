export class ContactRequest {
  id: string;
  toUserId: string;
  userId: string;
  fromUserName: string;
  toUserName: string;
  status: ContactRequestStatus;
  isUserOwner: boolean;
}

export enum ContactRequestStatus {
  Open = 0,
  Approved,
  Rejected
}
