import { MasterValueItem } from "./master-value-item";

export class MainProfileInfo {
  isActivated: boolean;
  name: string;
  groupName: string;
  contactEmail: string;
  phone1: string;
  phone2: string;
  city: string;
  address: string;
  age: any;
  experience: number;
  description: string;
  photoBase64: string;
  workTypes: MasterValueItem[];
}
