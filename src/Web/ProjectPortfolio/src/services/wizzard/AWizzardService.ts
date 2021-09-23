export abstract class AWizzardService {
  abstract getForm(body: string): Promise<string>;
}
