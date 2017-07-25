import { SilpPage } from './app.po';

describe('silp App', () => {
  let page: SilpPage;

  beforeEach(() => {
    page = new SilpPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
