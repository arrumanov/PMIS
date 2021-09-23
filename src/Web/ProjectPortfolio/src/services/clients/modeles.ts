type IdentityDocument = {
  identificationNumber: string;
};

export type ClientDTO = {
  id: string;
  contextId: string;
  typeCode: string;
  contacts: any[];
  typeName: string;
  name: string;
  shortName: string;
  keyInfo: string;
  keyInfoTypeCode: string;
  keyInfoTypeName: string;
  resident: boolean;
  citizenshipCode: string;
  citizenshipName: string;
  identityDocuments: IdentityDocument[];
  email: string;
  manager: string;
  divisionCode: string;
  active: boolean;
  activeChangeDate: string;
  activeChangeUser: string;
  srcCode: string;
  srcId: string;
  srcCodeName: string;
  create_date: string;
  create_user: string;
  change_date: string;
  change_user: string;
};

export type DetailDTO = {
  id: string;
  name: string;
  email: string;
  manager: string;
  divisionCode: string;
};
