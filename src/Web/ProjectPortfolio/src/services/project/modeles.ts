import internal from "stream";

export interface DictionaryValue{
    id: string;
    dictionaryKey: string;
    name: string;
    isActive: string;
    code: string;
    sequence: string;
}

export interface User{
    id: string;
    smallName: string;
}

export interface Department{
    id: string;
    name: string;
}

export interface Product{
    id: string;
    name: string;
}

export interface Contragent{
    id: string;
    name: string;
}

export interface ProjectDepartment{
    departmentId: string;
    department: Department;
}

export interface ProjectProduct{
    productId: string;
    product: Product;
}

export interface ProjectContragent{
    contragentId: string;
    contragent: Contragent;
}

// export interface Project{
//     id: string;
//     name: string;
//     description: string;
//     responsibleDepartmentId: string;
//     responsibleDepartment: Department;
//     categoryId: string;
//     typeId: string;
//     category: DictionaryValue;
//     type: DictionaryValue;
//     initiatorId: string;
//     curatorId: string;
//     managerId: string;
//     creatorId: string;
//     createdDate: internal;
//     projectDepartments: [ProjectDepartment];
//     projectProducts: [ProjectProduct];
//     projectContragents: [ProjectContragent];
//     initiator: User;
//     curator: User;
//     manager: User;
//     creator: User;
// }

// export interface Projects{
//     projects: Project[];
// }

export interface Project {
    __typename: "Project";
    id: string | null;
    name: string | null;
    description: string | null;
  }
  
  export interface Projects {
    projects: Project[];
  }