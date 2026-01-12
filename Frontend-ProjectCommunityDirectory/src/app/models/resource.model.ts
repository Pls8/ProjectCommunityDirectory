import { ResourceCreate } from './resource-create.model' 

export interface Resource extends ResourceCreate {
  id: number;
  isApproved: boolean;
  categoryName: string;
}

