import * as fromInvoice from '../actions/invoice-detail.action';
import { Order } from '../../order.model';

export interface InvoiceDetailState {
    data: Order,
    loaded: boolean,
    loading: boolean
}

export const initialState: InvoiceDetailState = {
    data: null,
    loaded: false,
    loading: false
}

export function reducer(state = initialState, action: fromInvoice.InvoiceDetailAction): InvoiceDetailState {
    switch (action.type) {
        case fromInvoice.INVOICE_DETAIL_LOAD: {
            return {
                ...state,
                loading: true
            };
        }
        case fromInvoice.INVOICE_DETAIL_LOAD_SUCCESS: {

            return {
                ...state,
                data: action.payload,
                loading: false,
                loaded: true
            };
        }

        case fromInvoice.INVOICE_DETAIL_LOAD_FAIL: {
            return {
                ...state,
                loading: false,
                loaded: false
            };
        }

        case fromInvoice.INVOICE_DETAIL_SAVE: {
            return {
                ...state,
                loading: true,
            };
        }

        case fromInvoice.INVOICE_DETAIL_SAVE_SUCCESS: {
            return {
                ...state,
                data: action.payload,
                loading: false,
                loaded: true
            };
        }

        case fromInvoice.INVOICE_DETAIL_SAVE_FAIL: {
            return {
                ...state,
                loading: false,
                loaded: false
            };
        }
    }

    return state;
}


