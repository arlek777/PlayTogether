import { MasterValueItem } from "./master-value-item";

export class MainProfileInfo {
  isActivated: boolean;
  name: string;
  groupName: string;
  age: any;
  experience: number;
  description: string;
  photoBase64: string;
  workTypes: MasterValueItem[];
  musicGenres: MasterValueItem[];
  musicianRoles: MasterValueItem[];
  hasMusicianEducation: boolean;
}
