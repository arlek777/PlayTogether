import { MasterValueItem } from "./master-value-item";
import { MainProfileInfo } from "./main-profile-info";

export class PublicProfile {
  id: string;
  name: string;
  groupName: string;
  age: any;
  experience: number;
  description: string;
  photoBase64: string;
  workTypes: MasterValueItem[];
  musicGenres: MasterValueItem[];
  musicianRoles: MasterValueItem[];

  //contacts
  contactEmail: string;
  phone1: string;
  phone2: string;
  url1: string;
  url2: string;
  city: string;
  address: string;
  contactTypes: MasterValueItem[];

  isContactsAvailable: boolean;
  isContactRequestSent: boolean;
  isSelfProfile: boolean;
}
