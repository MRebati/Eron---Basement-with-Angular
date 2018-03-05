import { XHRBackend, RequestOptions } from '@angular/http';

import { HttpService} from './http.service';

export function HttpFactory(backend: XHRBackend, options: RequestOptions) {
    return new HttpService(backend, options);
}
