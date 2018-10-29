import { MasterValueItem } from "./master-value-item";
import { MainProfileInfo } from "./main-profile-info";

export class PublicProfile extends MainProfileInfo {
  musicGenres: MasterValueItem[];
  musicianRoles: MasterValueItem[];
}
