import axios from 'axios'

import { ROOT_URL } from 'actions/environment'

const API = 'api/ReportingItems';

export function fetchReportingItems(id) {
  const request = axios.get(`${ROOT_URL}api/ContractReportingItem/${id}`)
  return request
}

export function fetchReportingItem(id) {
  const request = axios.get(`${ROOT_URL}${API}/${id}`)
  return request
}

export function createReportingItem(props) {
  const request = axios.post(`${ROOT_URL}${API}`, props)
  return request
}

export function updateReportingItem(props) {
  const request = axios.put(`${ROOT_URL}${API}/${props.id}`, props)
  return request
}

export function deleteReportingItem(id) {
  const request = axios.delete(`${ROOT_URL}${API}/${id}`)
  return request
}