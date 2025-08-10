import { TestBed } from '@angular/core/testing';

import { SocialBlogAPIService } from './social-blog-api.service';

describe('SocialBlogAPIService', () => {
  let service: SocialBlogAPIService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SocialBlogAPIService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
