export enum VacancyStatus {
  Active = 0,
  NotActive,
  Closed
}

export class Vacancy {
  id: string;
  title: string;
  status: VacancyStatus;
}

export class VacancyDetail extends Vacancy {
  searchFilterId: string;
  description: string;
  vacancyFilter: VacancyFilter;
}

export class VacancyFilter {
  id: string;
  minRating: number;
  minExpirience: number;
  cities: string[];
  musicGenreIds: string[];
  musicianRoleIds: string[];
  workTypeIds: string[];
}
